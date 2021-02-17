using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VSConventionalCommitMessage
{
    public class OptionPageGrid : DialogPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Category( "Commit Message Helper" )]
        [DisplayName( "Commit Types" )]
        [Description( "List of commit types" )]
        public string CommitTypes
        {
            get => commitTypes;
            set
            {
                commitTypes = value;
                OnPropertyChanged();
            }
        }
        [Category( "Commit Message Helper" )]
        [DisplayName( "Scopes" )]
        [Description( "List of scopes" )]
        public string Scopes
        {
            get => scopes;
            set
            {
                scopes = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged( [CallerMemberName] string name = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( name ) );
        }

        private string commitTypes = "feat,fix,refactor,style,test,docs,chore";
        private string scopes = ",ui,plugin,temlate,driver,validation";
    }
}
