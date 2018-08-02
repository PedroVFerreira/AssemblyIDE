using System;

namespace Componentes
{
    public class Flag
    {
        private bool _flagValue = false;
        private string _flagName;

        public Flag(bool flagValue, string flagName)
        {
            _flagName = flagName;
            _flagValue = flagValue;
        }

        public void setFlagValue(bool value)
        {
            _flagValue = value;
        }

        public bool getFlagValue()
        {
            return _flagValue;
        }

        public string getFlagName()
        {
            return _flagName == null ? "" : _flagName;
        }

        public string toString()
        {
            var name = _flagName == null ? "" : _flagName;
            
            return "Flag " + name + " : " + _flagValue;
        }
    }
}
