namespace Daylio_Parser
{
    internal class DaylioShellEventArgs<TEventResult> : EventArgs
    {
        public TEventResult? Result { get; set; }
    }
}
