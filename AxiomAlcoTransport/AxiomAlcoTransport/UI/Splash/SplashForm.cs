using DevExpress.XtraWaitForm;

namespace Axiom.AlcoTransport
{
    public partial class SplashForm : WaitForm
    {
        public SplashForm()
        {
            InitializeComponent();
            Splash_progressPanel.AutoHeight = true;
        }

        #region Overrides
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            Splash_progressPanel.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            Splash_progressPanel.Description = description;
        }
        #endregion
    }
}