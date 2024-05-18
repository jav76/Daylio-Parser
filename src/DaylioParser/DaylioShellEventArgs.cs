namespace DaylioParser
{

    // I don't really like how the typing of the event args is done here. We can only use an array of the same type?
    // Should I maybe make a separaate EventArgs class for each type of event that inherits this? This is a bit too generic
    internal class DaylioShellEventArgs<TEventResult, TEventArgs>(TEventArgs[] args) : EventArgs
    {
        public TEventResult? Result { get; set; }

        public TEventArgs[] Args { get; set; } = args;
    }

    internal class DaylioShellEventArgs<TEventResult> : EventArgs
    {
        public TEventResult? Result { get; set; }
    }
}
