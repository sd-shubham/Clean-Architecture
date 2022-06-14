using System.Runtime.CompilerServices;

namespace App.Application.Helper
{
    [InterpolatedStringHandler]
    internal ref struct GenericStringHandler<T>
    {
        private DefaultInterpolatedStringHandler _handler;
        internal GenericStringHandler(int literalLength, int formattedCount,
                                          T obj, out bool shouldAppend)
        {
           //ToDo - changes requires check other type as well.
           if(obj is bool val)
            {
                if (val)
                {
                    _handler = default;
                    shouldAppend = false;
                    return;
                }
            }
           else if (obj is not null) 
            {
                _handler = default;
                shouldAppend = false;
                return;
            }
            _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
            shouldAppend = true;
        }
        public override string ToString()
        {
            return _handler.ToString();
        }
        public string ToStringAndClear()
        {
            return _handler.ToStringAndClear();
        }
        public void AppendLiteral(string message)
        {
            _handler.AppendLiteral(message);
        }
        public void AppendFormatted<T>(T message)
        {
            _handler.AppendFormatted(message);
        }
    }
}
