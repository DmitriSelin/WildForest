using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Marks;
using WildForest.Frontend.Services.Marks.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal partial class CommentsViewModel : ObservableObject
    {
        private readonly IMarkService _markService;
        private string token;
        private Guid weatherId;

        internal void FillData(string token, Guid weatherId)
        {
            this.token = token;
            this.weatherId = weatherId;
        }

        #region Properties

        [ObservableProperty]
        private byte[] marks = {1, 2, 3, 4, 5 };

        [ObservableProperty]
        private object? selectedMark;

        [ObservableProperty]
        private List<CommentsModel> comments;

        #endregion

        #region Commands

        #region DownloadCommentsCommand

        public IAsyncRelayCommand DownloadCommentsCommand { get; }

        private bool isFirstLoaded = true;

        private async Task DownloadCommentsAsync()
        {
            if (!isFirstLoaded)
                return;

            var markResponse = await _markService.GetMarksAsync(weatherId, token);

            if (markResponse.Comments is not null)
            {
                Comments = markResponse.Comments;
            }
            else
            {
                MessageBox.Show(markResponse.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            isFirstLoaded = false;
        }

        #endregion

        #endregion

        public CommentsViewModel(IMarkService markService)
        {
            _markService = markService;
            DownloadCommentsCommand = new AsyncRelayCommand(DownloadCommentsAsync);
        }
    }
}