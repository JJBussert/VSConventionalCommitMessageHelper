using System.Windows.Controls;

namespace VSConventionalCommitMessage
{
    /// <summary>
    /// Interaction logic for CommitMessageWindowControl.
    /// </summary>
    public partial class CommitMessageWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitMessageWindowControl"/> class.
        /// </summary>
        public CommitMessageWindowControl( VSConventionalCommitMessagePackage package )
        {
            InitializeComponent();

            viewModel = new CommitMessageViewModel( package.OptionPage );

            DataContext = viewModel;
        }

        private readonly CommitMessageViewModel viewModel;
    }
}