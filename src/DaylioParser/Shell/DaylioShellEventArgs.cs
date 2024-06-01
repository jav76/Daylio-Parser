namespace DaylioParser.Shell
{
    internal class DaylioShellEventArgs<TEventResult, TEventArgs> : EventArgs
    {
        public TEventResult? Result { get; set; }

        public TEventArgs[] Args { get; set; }

        public DaylioShellEventArgs(TEventArgs[] args)
        {
            Args = args;
        }
    }

    internal class DaylioShellEventArgs<TEventResult> : EventArgs
    {
        public TEventResult? Result { get; set; }
    }
}
