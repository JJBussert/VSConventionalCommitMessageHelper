using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace VSConventionalCommitMessage
{
    public class CommitMessageViewModel : ViewModelBase
    {
        #region CommitTypes
        public IList<string> CommitTypes
        {
            get => new List<string>
            {
                "feat",
                "fix",
                "refactor",
                "style",
                "test",
                "docs",
                "chore"
            };
        }

        public string SelectedCommitType
        {
            get => selectedCommitType;
            set
            {
                Set( nameof( SelectedCommitType ), ref selectedCommitType, value );
                RaisePropertyChanged( nameof( GeneratedCommitMessage ) );
            }
        }

        private string selectedCommitType;

        #endregion

        #region Scope

        public IEnumerable<string> Scopes
        {
            get => scopes;
        }

        public string SelectedScope
        {
            get => selectedScope;
            set
            {
                Set( nameof( SelectedScope ), ref selectedScope, value );
                RaisePropertyChanged( nameof( GeneratedCommitMessage ) );
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
                    scopes.Add( s );
                    SelectedScope = s;
                }
            }
        }

        private readonly ObservableCollection<string> scopes = new ObservableCollection<string>
        {
            "",
            "ui",
            "plugin",
            "template",
            "driver",
            "validation"
        };

        private string selectedScope = "";

        #endregion

        #region Subject

        public string Subject
        {
            get => subject;
            set
            {
                Set( nameof( Subject ), ref subject, value );
                RaisePropertyChanged( nameof( GeneratedCommitMessage ) );
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
                Set( nameof( Description ), ref description, value );
                RaisePropertyChanged( nameof( GeneratedCommitMessage ) );
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
                Set( nameof( Closes ), ref closes, value );
                RaisePropertyChanged( nameof( GeneratedCommitMessage ) );
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
                else
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

        public ICommand ClearCommand
        {
            get => new RelayCommand( Clear );
        }

        public void Clear()
        {
            SelectedCommitType = null;
            SelectedScope = null;
            Subject = null;
            Description = null;
            Closes = null;
        }
    }
}
