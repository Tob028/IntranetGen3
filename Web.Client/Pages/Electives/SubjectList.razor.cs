﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Components.Web;
using MensaGymnazium.IntranetGen3.Contracts;
using Microsoft.AspNetCore.Components;
using Havit.Collections;
using MensaGymnazium.IntranetGen3.Web.Client.Services.DataStores;

namespace MensaGymnazium.IntranetGen3.Web.Client.Pages.Electives
{
	public partial class SubjectList
	{
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected ISubjectFacade SubjectFacade { get; set; }
		[Inject] protected NavigationManager NavigationManager { get; set; }
		[Inject] protected ISubjectCategoriesDataStore SubjectCategoriesDataStore { get; set; }
		[Inject] protected ISubjectTypesDataStore SubjectTypesDataStore { get; set; }

		private SubjectListQueryFilter subjectListFilter = new SubjectListQueryFilter();
		private HxGrid<SubjectListItemDto> subjectsGrid;
		private SubjectListItemDto subjectSelected;
		private SubjectEdit subjectEditComponent;

		protected override async Task OnInitializedAsync()
		{
			await SubjectCategoriesDataStore.EnsureDataAsync();
			await SubjectTypesDataStore.EnsureDataAsync();
		}

		private async Task<GridDataProviderResult<SubjectListItemDto>> LoadSubjects(GridDataProviderRequest<SubjectListItemDto> request)
		{
			var subjectListRequest = new DataFragmentRequest<SubjectListQueryFilter>()
			{
				Filter = subjectListFilter,
				StartIndex = request.StartIndex,
				Count = request.Count,
				Sorting = request.Sorting?.Select(s => new SortItem(s.SortString, s.SortDirection)).ToArray()
			};

			var subjectListResult = await SubjectFacade.GetSubjectListAsync(subjectListRequest, request.CancellationToken);

			return new()
			{
				Data = subjectListResult.Data ?? new(),
				TotalCount = subjectListResult.TotalCount
			};
		}

		private Task HandleSelectedDataItemChanged(SubjectListItemDto selection)
		{
			subjectSelected = selection;
			NavigationManager.NavigateTo(Routes.Electives.GetSubjectDetail(selection.SubjectId));
			return Task.CompletedTask;
		}

		private async Task HandleDeleteItemClicked(SubjectListItemDto subject)
		{
			await SubjectFacade.DeleteSubjectAsync(Dto.FromValue(subject.SubjectId));
			Messenger.AddInformation(subject.Name, "Předmět smazán.");
			await subjectsGrid.RefreshDataAsync();
		}

		private async Task HandleFilterModelChanged(SubjectListQueryFilter newFilterModel)
		{
			subjectListFilter = newFilterModel;
			await subjectsGrid.RefreshDataAsync();
		}

		private async Task HandleNewItemClicked()
		{
			await subjectEditComponent.ShowAsync();
		}

		private async Task HandleSubjectEditClosed()
		{
			await subjectsGrid.RefreshDataAsync();
		}

		private string GetSubjectTypes(List<int> subjectTypesIds)
		{
			return String.Join(", ", subjectTypesIds.Select(id => SubjectTypesDataStore.GetByKey(id)?.Name))
				.Trim(',', ' ');
		}
	}
}
