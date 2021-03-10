using System.Collections.Generic;
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
                if ( selectedScope != value )
                {
                    selectedScope = value;
                    OnPropertyChanged();
                    OnPropertyChanged( nameof( GeneratedCommitMessage ) );
                }
            }
        }

        public string NewScope
        {
            set
            {
                var s = value.Trim();
                SelectedScope = s;
            }
        }

        private string selectedScope = "";

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

        #region Body

        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string body;

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

        #region BreakingChange

        public string BreakingChange
        {
            get => breakingChange;
            set
            {
                breakingChange = value;
                OnPropertyChanged();
                OnPropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string breakingChange;

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

                message += Description?.Trim();

                if ( string.IsNullOrEmpty( Body ) == false )
                {
                    message += $"\n\n{Body.Trim()}";
                }

                message += "\n";

                if ( string.IsNullOrEmpty( Closes ) == false )
                {
                    message += $"\nCloses {Closes.Trim()}";
                }

                if ( string.IsNullOrEmpty( BreakingChange ) == false )
                {
                    message += $"\nBREAKING CHANGE: {BreakingChange.Trim()}";
                }

                return message.Trim();
            }
        }

        public ICommand CopyCommand
        {
            get => new RelayCommand(
                () => Clipboard.SetText( GeneratedCommitMessage ),
                () => string.IsNullOrEmpty( SelectedCommitType ) == false && string.IsNullOrEmpty( Description ) == false );
        }

        public ICommand ClearCommand
        {
            get => new RelayCommand( () =>
                {
                    SelectedCommitType = null;
                    SelectedScope = "";
                    Description = null;
                    Body = null;
                    Closes = null;
                    BreakingChange = null;
                },
                () => string.IsNullOrEmpty( GeneratedCommitMessage ) == false );
        }

        private readonly OptionPageGrid optionPage;
    }
}
