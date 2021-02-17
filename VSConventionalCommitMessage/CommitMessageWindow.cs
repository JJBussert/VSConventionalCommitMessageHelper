using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace VSConventionalCommitMessage
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid( "4e075324-ebc7-4eec-9edf-1cf2098ba81f" )]
    public class CommitMessageWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitMessageWindow"/> class.
        /// </summary>
        public CommitMessageWindow() : base( null )
        {
            this.Caption = "Commit Message Helper";
        }

        protected override void Initialize()
        {
            base.Initialize();

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new CommitMessageWindowControl( Package as VSConventionalCommitMessagePackage );
        }
    }
}
