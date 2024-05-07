﻿using Havit.Data.EntityFrameworkCore.Attributes;

namespace MensaGymnazium.IntranetGen3.Model;

[Cache]
public record GradeRegistrationCriteria
{
	public int Id { get; set; }

	/// <summary>
	/// What is total of donated hours needed.
	/// The hours donated by languages are not counted in!
	/// </summary>
	public int RequiredTotalAmountOfHoursPerWeekExcludingLanguage { get; set; }

	/// <summary>
	/// Does the rule about needing N subjects from ČSP or ČP areas apply?
	/// For 2023-2024 this applied to Septima, Oktava
	/// </summary>
	public bool RequiresCspOrCpValidation { get; set; }

	/// <summary>
	/// How many hours in educational field Csp/cP
	/// </summary>
	public int RequiredAmountOfHoursPerWeekInAreaCspOrCp { get; set; }

	/// <summary>
	/// Does the student need to have a foreign language
	/// </summary>
	public bool RequiresForeginLanguage { get; set; }

	public bool CanUseForeignLanguageInsteadOfHoursPerWeek { get; set; }
}