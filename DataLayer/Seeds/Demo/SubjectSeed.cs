﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using MensaGymnazium.IntranetGen3.DataLayer.Repositories.Security;
using MensaGymnazium.IntranetGen3.Model;


namespace MensaGymnazium.IntranetGen3.DataLayer.Seeds.Demo
{
	public class SubjectSeed : DataSeed<DemoProfile>
	{
		private readonly IUserRepository userRepository;

		public SubjectSeed(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public override void SeedData()
		{
			var users = userRepository.GetAll().Where(u => u.TeacherId is not null);

			var subjects = new[]
			{
				new Subject()
				{
					Capacity = 12,
					Name = "Tohle je stoprocentně reálnej předmět...",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Reálnej seminář",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block5,
					CategoryId = (int)SubjectCategory.Entry.Seminars,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.ArtCulture },
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.Informatics },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Zimen")).TeacherId.Value },
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Haken")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvarta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 13,
					Name = "Brdek je bůh.",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Seminář pravdy",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block1,
					CategoryId = (int)SubjectCategory.Entry.SpecialSeminars,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.HumanSociety },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Brdek")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sekunda },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Tercie },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvarta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 9,
					Name = "Kepler",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Kepler je odpad",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block4,
					CategoryId = (int)SubjectCategory.Entry.Seminars,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.ArtCulture },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Zimen")).TeacherId.Value },
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Haken")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sekunda },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Tercie },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvarta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "MatPol",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Stovky zdarma :-)",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block3,
					CategoryId = (int)SubjectCategory.Entry.Graduational,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.HumanSociety },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Svoboda")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "MatBio",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Kdo by nechtěl předmět s Jurčákovou?",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block2,
					CategoryId = (int)SubjectCategory.Entry.Graduational,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.HumanNature },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Jurčáková")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "SocAJ",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Není vhodný pro slabší povahy",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block4,
					CategoryId = (int)SubjectCategory.Entry.ForeignLanguage,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.HumanSociety },
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.LanguageCommunication },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Silva")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "ArdPrg",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Kočí > Šebestík",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block4,
					CategoryId = (int)SubjectCategory.Entry.SpecialSeminars,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.Informatics },
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.MathApplication },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Šebestík")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "SemPrg II",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Student = Levná pracovní síla",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block5,
					CategoryId = (int)SubjectCategory.Entry.SpecialSeminars,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.Informatics },
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.MathApplication },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Haken")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Tercie },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvarta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
				new Subject()
				{
					Capacity = 12,
					Name = "ŘímAJ",
					ScheduleDayOfWeek = DayOfWeek.Monday,
					Description = "Stejně se to neotevře ;-)",
					ScheduleSlotInDay = Primitives.ScheduleSlotInDay.Block5,
					CategoryId = (int)SubjectCategory.Entry.ForeignLanguage,
					TypeRelations =
					{
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.LanguageCommunication },
						new SubjectTypeRelation() { SubjectTypeId = (int)SubjectType.Entry.HumanWork },
					},
					TeacherRelations =
					{
						new SubjectTeacherRelation() { TeacherId = users.First(t => t.Name.Contains("Parker")).TeacherId.Value },
					},
					GradeRelations =
					{
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Kvinta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Sexta },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Septima },
						new SubjectGradeRelation() { GradeId = (int)Grade.Entry.Oktava },
					}
				},
			};

			Seed(For(subjects).PairBy(subject => subject.Name)
				.AfterSave(item => item.SeedEntity.TeacherRelations.ForEach(tr => tr.SubjectId = item.PersistedEntity.Id))
				.AfterSave(item => item.SeedEntity.TypeRelations.ForEach(tr => tr.SubjectId = item.PersistedEntity.Id))
				.AfterSave(item => item.SeedEntity.GradeRelations.ForEach(gr => gr.SubjectId = item.PersistedEntity.Id))
				.AndForAll(subject => subject.TeacherRelations, configuration => configuration.PairBy(tr => tr.SubjectId, tr => tr.TeacherId))
				.AndForAll(subject => subject.TypeRelations, configuration => configuration.PairBy(tr => tr.SubjectId, tr => tr.SubjectTypeId))
				.AndForAll(subject => subject.GradeRelations, configuration => configuration.PairBy(gr => gr.SubjectId, gr => gr.GradeId))
			);
		}

		public override IEnumerable<Type> GetPrerequisiteDataSeeds()
		{
			yield return typeof(TeacherSeed);
		}
	}
}