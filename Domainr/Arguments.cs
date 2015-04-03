using System.Collections.Generic;
using System.Dynamic;

namespace Domainr
{
    public class Arguments : DynamicObject
    {
        private Arguments()
        {
        }

        public static dynamic Build
        {
            get { return new Arguments(); }
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var arguments = new Argument[args.Length];
            var stack = new Stack<string>(binder.CallInfo.ArgumentNames);

            for (var i = args.Length - 1; i >= 0; i--)
            {
                var value = args[i];
                var key = stack.Count > 0 ? stack.Pop() : "value." + i;

                arguments[i] = new Argument(key, value);
            }

            result = arguments;
            return true;
        }
    }
}