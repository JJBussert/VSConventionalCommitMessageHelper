﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using VSConventionalCommitMessage.Base;

namespace VSConventionalCommitMessage
{
    public class CommitMessageViewModel : ViewModelBase
    {
        public CommitMessageViewModel( OptionPageGrid optionPage )
        {
            this.optionPage = optionPage;

            if ( optionPage != null )
            {
                optionPage.PropertyChanged += ( s, e ) =>
                {
                    OnPropertyChanged( e.PropertyName );
                };
            }
        }

        #region CommitTypes
        public IList<string> CommitTypes
        {
            get => optionPage?.CommitTypes?.Split( ',' );
        }

        public string SelectedCommitType
        {
            get => selectedCommitType;
            set
            {
                selectedCommitType = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string selectedCommitType;

        #endregion

        #region Scope

        public IEnumerable<string> Scopes
        {
            get => optionPage?.Scopes?.Split( ',' );
        }

        public string SelectedScope
        {
            get => selectedScope;
            set
            {
                selectedScope = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        public string NewScope
        {
            set
            {
                if ( SelectedScope != null )
                {
                    return;
                }
                if ( !string.IsNullOrEmpty( value ) )
                {
                    var s = value.Trim();
                    SelectedScope = s;
                }
            }
        }

        private string selectedScope = "";

        #endregion

        #region Subject

        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string subject;

        #endregion

        #region Description

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string description;

        #endregion

        #region Closes

        public string Closes
        {
            get => closes;
            set
            {
                closes = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string closes;

        #endregion

        public string GeneratedCommitMessage
        {
            get
            {
                string message = SelectedCommitType?.Trim() ?? "";

                if ( string.IsNullOrEmpty( SelectedScope ) == false )
                {
                    message += $"({SelectedScope.Trim()}): ";
                }
                else if ( string.IsNullOrEmpty( SelectedCommitType ) == false )
                {
                    message += ": ";
                }

                message += Subject?.Trim();

                if ( string.IsNullOrEmpty( Description ) == false )
                {
                    message += $"\n\n{Description.Trim()}";
                }

                if ( string.IsNullOrEmpty( Closes ) == false )
                {
                    message += $"\n\nCloses {Closes.Trim()}";
                }

                return message;
            }
        }

        public ICommand CopyCommand
        {
            get => new RelayCommand(
                () => Clipboard.SetText( GeneratedCommitMessage ),
                () => string.IsNullOrEmpty( SelectedCommitType ) == false && string.IsNullOrEmpty( Subject ) == false );
        }

        public ICommand ClearCommand
        {
            get => new RelayCommand( () =>
                {
                    SelectedCommitType = null;
                    SelectedScope = null;
                    Subject = null;
                    Description = null;
                    Closes = null;
                },
                () => string.IsNullOrEmpty( GeneratedCommitMessage ) == false );
        }

        private readonly OptionPageGrid optionPage;
    }
}
