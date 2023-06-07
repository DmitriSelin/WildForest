using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Marks;
using WildForest.Frontend.Services.Marks.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal partial class CommentsViewModel : ObservableObject
    {
        private readonly IMarkService _markService;
        private string token = null!;
        private Guid userId;

        internal void FillData(string token, Guid userId)
        {
            this.token = token;
            this.userId = userId;
        }

        #region Properties

        [ObservableProperty]
        private byte[] marks = {1, 2, 3, 4, 5 };

        [ObservableProperty]
        private object? selectedMark;

        [ObservableProperty]
        private ObservableCollection<CommentsModel> comments = new();

        [ObservableProperty]
        private string message = null!;

        [ObservableProperty]
        private bool isElementsEnabled = true;

        [ObservableProperty]
        private string mediumMark;

        #endregion

        #region Commands

        #region DownloadCommentsCommand

        public IAsyncRelayCommand DownloadCommentsCommand { get; }

        private bool isFirstLoaded = true;

        private async Task DownloadCommentsAsync()
        {
            if (!isFirstLoaded)
                return;

            var markResponse = await _markService.GetMarksAsync(WeatherViewModel.CurrentWeatherId, token);

            if (markResponse.Comments is not null)
            {
                FillComments(markResponse.Comments);
                MediumMark = Math.Round(WeatherViewModel.MediumMark, 2).ToString();
            }
            else
            {
                MessageBox.Show(markResponse.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            isFirstLoaded = false;
        }

        #endregion

        #region CommentCommand

        public IAsyncRelayCommand CommentCommand { get; }

        private async Task AddCommentAsync()
        {
            if (SelectedMark == null)
            {
                MessageBox.Show("Select your mark to send message!", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            byte rating = (byte)SelectedMark;

            if (Message == null)
            {
                MessageBox.Show("Write something to send message", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            Message = Message.Trim();

            if(Message == string.Empty)
            {
                MessageBox.Show("Write something to send message", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var request = new CommentRequest(WeatherViewModel.CurrentWeatherId, userId, rating, Message);
            var commentResponseBase = await _markService.AddCommentWithMarkAsync(request, token);

            if (commentResponseBase.Comment is not null)
            {
                var comment = commentResponseBase.Comment;
                var newComment = new CommentsModel(comment.MarkId, comment.UserId, WeatherViewModel.CurrentWeatherId, comment.Date, rating, "", "Me", comment.Comment);
                MediumMark = Math.Round(comment.MediumMark, 2).ToString();
                Comments.Add(newComment);
                IsElementsEnabled = false; 
                Message = string.Empty;
            }
            else
            {
                MessageBox.Show(commentResponseBase.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        #endregion

        public CommentsViewModel(IMarkService markService)
        {
            _markService = markService;
            DownloadCommentsCommand = new AsyncRelayCommand(DownloadCommentsAsync);
            CommentCommand = new AsyncRelayCommand(AddCommentAsync);
        }

        private void FillComments(List<CommentsModel> comments)
        {
            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }
    }
}