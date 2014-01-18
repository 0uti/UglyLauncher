using System;

namespace UglyLauncher.Minecraft
{

    class MCInvalidCredentialsException : Exception
    {
        public MCInvalidCredentialsException() : base("mööp.") { }
        public MCInvalidCredentialsException(Exception e) : base("mööp.", e) { }
        public MCInvalidCredentialsException(String message, Exception e) : base(message, e) { }
        public MCInvalidCredentialsException(String message) : base(message) { }
    }

    class MCUserMigratedException : Exception
    {
        public MCUserMigratedException() : base("mööp.") { }
        public MCUserMigratedException(Exception e) : base("mööp.", e) { }
        public MCUserMigratedException(String message, Exception e) : base(message, e) { }
        public MCUserMigratedException(String message) : base(message) { }
    }

    class MCInvalidTokenException : Exception
    {
        public MCInvalidTokenException() : base("mööp.") { }
        public MCInvalidTokenException(Exception e) : base("mööp.", e) { }
        public MCInvalidTokenException(String message, Exception e) : base(message, e) { }
        public MCInvalidTokenException(String message) : base(message) { }
    }
}
