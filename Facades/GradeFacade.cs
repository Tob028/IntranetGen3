﻿using MensaGymnazium.IntranetGen3.Contracts;
using MensaGymnazium.IntranetGen3.DataLayer.Repositories;
using MensaGymnazium.IntranetGen3.Model;
using MensaGymnazium.IntranetGen3.Primitives;

namespace MensaGymnazium.IntranetGen3.Facades;

[Service]
[Authorize]
public class GradeFacade : IGradeFacade
{
	private readonly IGradeRepository _gradeRepository;
	private readonly IUnitOfWork _unitOfWork;

	public GradeFacade(IGradeRepository gradeRepository, IUnitOfWork unitOfWork)
	{
		_gradeRepository = gradeRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<List<GradeDto>> GetAllGradesAsync(CancellationToken cancellationToken = default)
	{
		var data = await _gradeRepository.GetAllAsync(cancellationToken);

		return data
			.Select(g => new GradeDto()
			{
				Id = g.Id,
				Name = g.Name
			})
			.ToList();
	}

	[Authorize(Roles = nameof(Role.Administrator))]
	public async Task<List<GradeRegistrationCriteriaDto>> GetGradeRegistrationCriteriasAsync(CancellationToken cancellationToken = default)
	{
		// Only ever get 8 elements, so we can load all to memory
		var grades = await _gradeRepository.GetAllAsync(cancellationToken);

		return grades
			.Select(g => new GradeRegistrationCriteriaDto()
			{
				GradeId =
					g.Id,

				CanUseForeignLanguageInsteadOfHoursPerWeek =
					g.RegistrationCriteria.CanUseForeignLanguageInsteadOfHoursPerWeek,

				RequiredAmountOfHoursPerWeekInAreaCsOrCp =
					g.RegistrationCriteria.RequiredAmountOfHoursPerWeekInAreaCsOrCp,

				RequiredTotalAmountOfHoursPerWeekExcludingLanguage =
					g.RegistrationCriteria.RequiredTotalAmountOfHoursPerWeekExcludingLanguage,

				RequiresCsOrCpValidation =
					g.RegistrationCriteria.RequiresCsOrCpValidation,

				RequiresForeignLanguage =
					g.RegistrationCriteria.RequiresForeginLanguage
			})
			.ToList();
	}

	[Authorize(Roles = nameof(Role.Administrator))]
	public async Task UpdateGradeRegistrationCriteriaAsync(GradeRegistrationCriteriaDto model, CancellationToken cancellationToken = default)
	{
		Contract.Requires<ArgumentNullException>(model is not null);
		if (model.CanUseForeignLanguageInsteadOfHoursPerWeek && model.RequiresForeignLanguage)
		{
			// Xopa: Maybe app logic is leaking and this should be a service?
			throw new InvalidOperationException("Ročník nemůže vyžadovat jazyk a zároveň ho využít namísto hodin v rozvrhu");
		}

		var grade = await _gradeRepository.GetObjectAsync(model.GradeId, cancellationToken);

		MapRegistrationCriteriaFromDTO(model, grade.RegistrationCriteria);

		_unitOfWork.AddForUpdate(grade);
		await _unitOfWork.CommitAsync(cancellationToken);
	}

	private void MapRegistrationCriteriaFromDTO(GradeRegistrationCriteriaDto dto, GradeRegistrationCriteria criteria)
	{
		criteria.CanUseForeignLanguageInsteadOfHoursPerWeek = dto.CanUseForeignLanguageInsteadOfHoursPerWeek;
		criteria.RequiredAmountOfHoursPerWeekInAreaCsOrCp = dto.RequiredAmountOfHoursPerWeekInAreaCsOrCp;
		criteria.RequiredTotalAmountOfHoursPerWeekExcludingLanguage = dto.RequiredTotalAmountOfHoursPerWeekExcludingLanguage;
		criteria.RequiresCsOrCpValidation = dto.RequiresCsOrCpValidation;
		criteria.RequiresForeginLanguage = dto.RequiresForeignLanguage;
	}
}
