using System.Runtime.CompilerServices;

namespace App.Application.Helper
{
    [InterpolatedStringHandler]
    internal ref struct StringHandler
    {
        private DefaultInterpolatedStringHandler _handler;
        internal StringHandler(int literalLength, int formattedCount,
                                          bool condition, out bool shouldAppend)
        {
            if (!condition)
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
