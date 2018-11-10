using System;

namespace UglyLauncher.Minecraft.Authentication
{
    [Serializable]
    class MCInvalidCredentialsException : Exception
    {
        public MCInvalidCredentialsException() : base("mööp.") { }
        public MCInvalidCredentialsException(Exception e) : base("mööp.", e) { }
        public MCInvalidCredentialsException(string message, Exception e) : base(message, e) { }
        public MCInvalidCredentialsException(string message) : base(message) { }
    }

    [Serializable]
    class MCUserMigratedException : Exception
    {
        public MCUserMigratedException() : base("mööp.") { }
        public MCUserMigratedException(Exception e) : base("mööp.", e) { }
        public MCUserMigratedException(string message, Exception e) : base(message, e) { }
        public MCUserMigratedException(string message) : base(message) { }
    }

    [Serializable]
    class MCInvalidTokenException : Exception
    {
        public MCInvalidTokenException() : base("mööp.") { }
        public MCInvalidTokenException(Exception e) : base("mööp.", e) { }
        public MCInvalidTokenException(string message, Exception e) : base(message, e) { }
        public MCInvalidTokenException(string message) : base(message) { }
    }
}
