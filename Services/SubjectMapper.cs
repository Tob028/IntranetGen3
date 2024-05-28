﻿using MensaGymnazium.IntranetGen3.Contracts;
using MensaGymnazium.IntranetGen3.DataLayer.Repositories;
using MensaGymnazium.IntranetGen3.Model;
using MensaGymnazium.IntranetGen3.Primitives;

namespace MensaGymnazium.IntranetGen3.Services;

[Service]
public class SubjectMapper : ISubjectMapper
{
	private readonly IDataLoader _dataLoader;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IStudentSubjectRegistrationRepository _studentSubjectRegistrationRepository;

	public SubjectMapper(
		IDataLoader dataLoader,
		IUnitOfWork unitOfWork,
		IStudentSubjectRegistrationRepository studentSubjectRegistrationRepository)
	{
		_dataLoader = dataLoader;
		_unitOfWork = unitOfWork;
		_studentSubjectRegistrationRepository = studentSubjectRegistrationRepository;
	}

	public async Task MapFromSubjectDtoAsync(SubjectDto subjectDto, Subject subject, CancellationToken cancellationToken = default)
	{
		Contract.Requires<ArgumentNullException>(subjectDto is not null);
		Contract.Requires<ArgumentNullException>(subject is not null);

		if (subject.Id != default)
		{
			await _dataLoader.LoadAsync(subject, s => s.TeacherRelations, cancellationToken);
			await _dataLoader.LoadAsync(subject, s => s.EducationalAreaRelations, cancellationToken);
			await _dataLoader.LoadAsync(subject, s => s.GradeRelations, cancellationToken);
			await _dataLoader.LoadAsync(subject, s => s.GraduationSubjectRelations, cancellationToken);
		}

		subject.Name = subjectDto.Name;
		subject.Description = subjectDto.Description;
		subject.CategoryId = subjectDto.CategoryId.Value;
		subject.Capacity = subjectDto.Capacity;
		subject.ScheduleDayOfWeek = subjectDto.ScheduleDayOfWeek.Value;
		subject.ScheduleSlotInDay = subjectDto.ScheduleSlotInDay.Value;
		subject.CanRegisterRepeatedly = subjectDto.CanRegisterRepeatedly;
		subject.HoursPerWeek = subjectDto.HoursPerWeek;
		subject.MinStudentsToOpen = subjectDto.MinStudentsToOpen;

		var teacherRelationsUpdateFromResult = subject.TeacherRelations.UpdateFrom(subjectDto.TeacherIds,
			targetKeySelector: t => t.TeacherId,
			sourceKeySelector: s => s,
			newItemCreateFunc: s => new SubjectTeacherRelation { SubjectId = subject.Id, TeacherId = s },
			updateItemAction: (s, t) => t.TeacherId = s,
			removeItemAction: t => { });
		_unitOfWork.AddUpdateFromResult(teacherRelationsUpdateFromResult);

		var typeRelationsUpdateFromResult = subject.EducationalAreaRelations.UpdateFrom(subjectDto.EducationalAreaIds,
			targetKeySelector: t => t.EducationalAreaId,
			sourceKeySelector: s => s,
			newItemCreateFunc: s => new EducationalAreaRelation { SubjectId = subject.Id, EducationalAreaId = s },
			updateItemAction: (s, t) => t.EducationalAreaId = s,
			removeItemAction: t => { });
		_unitOfWork.AddUpdateFromResult(typeRelationsUpdateFromResult);

		var graduationSubjectRelationsUpdateFromResult = subject.GraduationSubjectRelations.UpdateFrom(subjectDto.GraduationSubjectIds,
			targetKeySelector: t => t.GraduationSubjectId,
			sourceKeySelector: s => s,
			newItemCreateFunc: s => new GraduationSubjectRelation { SubjectId = subject.Id, GraduationSubjectId = s },
			updateItemAction: (s, t) => t.GraduationSubjectId = s,
			removeItemAction: t => { });
		_unitOfWork.AddUpdateFromResult(graduationSubjectRelationsUpdateFromResult);

		var gradeRelationsUpdateFromResult = subject.GradeRelations.UpdateFrom(subjectDto.GradeIds,
			targetKeySelector: t => t.GradeId,
			sourceKeySelector: s => s,
			newItemCreateFunc: s => new SubjectGradeRelation { SubjectId = subject.Id, GradeId = s },
			updateItemAction: (s, t) => t.GradeId = s,
			removeItemAction: t => { });
		_unitOfWork.AddUpdateFromResult(gradeRelationsUpdateFromResult);

	}

	public async Task<SubjectDto> MapToSubjectDtoAsync(Subject subject, CancellationToken cancellationToken = default)
	{
		Contract.Requires<ArgumentNullException>(subject is not null);

		await _dataLoader.LoadAsync(subject, s => s.TeacherRelations, cancellationToken);
		await _dataLoader.LoadAsync(subject, s => s.GradeRelations, cancellationToken);
		await _dataLoader.LoadAsync(subject, s => s.EducationalAreaRelations, cancellationToken);
		await _dataLoader.LoadAsync(subject, s => s.GraduationSubjectRelations, cancellationToken);

		var studentRegistrations = await _studentSubjectRegistrationRepository.GetBySubjectAsync(subject.Id, cancellationToken);

		return new SubjectDto
		{
			Id = subject.Id,
			Name = subject.Name,
			Description = subject.Description,
			CategoryId = subject.CategoryId,
			EducationalAreaIds = subject.EducationalAreaRelations.Select(tr => tr.EducationalAreaId).ToList(),
			GraduationSubjectIds = subject.GraduationSubjectRelations.Select(tr => tr.GraduationSubjectId).ToList(),
			Capacity = subject.Capacity,
			StudentRegistrationsCountMain = studentRegistrations.Count(ssr => ssr.RegistrationType == StudentRegistrationType.Main),
			StudentRegistrationsCountSecondary = studentRegistrations.Count(ssr => ssr.RegistrationType == StudentRegistrationType.Secondary),
			GradeIds = subject.GradeRelations.Select(tr => tr.GradeId).ToList(),
			TeacherIds = subject.TeacherRelations.Select(tr => tr.TeacherId).ToList(),
			ScheduleSlotInDay = subject.ScheduleSlotInDay,
			ScheduleDayOfWeek = subject.ScheduleDayOfWeek,
			CanRegisterRepeatedly = subject.CanRegisterRepeatedly,
			HoursPerWeek = subject.HoursPerWeek,
			MinStudentsToOpen = subject.MinStudentsToOpen
		};
	}
}
