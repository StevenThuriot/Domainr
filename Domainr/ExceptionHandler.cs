using System;

namespace Domainr
{
    [Serializable]
    class ExceptionHandler
    {
        private readonly Action<Exception> _handler;

        public ExceptionHandler(Action<Exception> handler)
        {
            _handler = handler;
        }

        public void Handle(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex == null) return;

            _handler(ex);
        }
    }
}