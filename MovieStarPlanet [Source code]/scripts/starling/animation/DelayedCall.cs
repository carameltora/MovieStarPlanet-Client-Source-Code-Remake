using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starling.animation
{
    internal class DelayedCall
    {
        public Int64 _currentTime;
        public Int64 _totalTime;
        public Byte[] _args;
        public static int _repeatCount;

        public void DelayedCallAsync(int Number, Array array = null)
        {
            reset(Number, array);
        }

        public void reset(int Number, Array array = null)
        {
            _currentTime = 0;
            _totalTime = (long)Math.Max(Number, 0.0001);
            _repeatCount = 1;
        }

        static void toPool(DelayedCall delayedCall)
        {
            delayedCall._args = null;
        }

        public void advanceTime(int Number)
        {
            string _loc2_ = null;
            byte[] _loc3_ = null;
            int _loc4_ = (int)_currentTime;
            _currentTime += Number;
            if(_currentTime > _totalTime)
            {
                _currentTime = _totalTime;
            }
            if(_loc4_ < _totalTime && _currentTime >= _totalTime)
            {
                if(_repeatCount == 0 || _repeatCount < 1)
                {
                    if(_repeatCount > 0)
                    {
                        --_repeatCount;
                    }
                    _currentTime = 0;
                    advanceTime((int)(_loc4_ + Number + _totalTime));
                }
                else
                {
                    _loc3_ = _args;
                }
            }
        }

        public void complete()
        {
            int _loc1_ = (int)(_totalTime - _currentTime);
            if(_loc1_ > 0)
            {
                advanceTime(_loc1_);
            }
        }
    }
}
