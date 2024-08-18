using System;

// This file is auto-generated. Do not modify or move this file.

namespace SuperUnityBuild.Generated
{
    public enum ReleaseType
    {
        None,
        Release,
        Prototyping,
    }

    public enum Platform
    {
        None,
        PC,
        macOS,
    }

    public enum ScriptingBackend
    {
        None,
        Mono,
    }

    public enum Architecture
    {
        None,
        Windows_x64,
        macOS,
    }

    public enum Distribution
    {
        None,
    }

    public static class BuildConstants
    {
        public static readonly DateTime buildDate = new DateTime(638595897315147560);
        public const string version = "v0.2";
        public const ReleaseType releaseType = ReleaseType.Release;
        public const Platform platform = Platform.macOS;
        public const ScriptingBackend scriptingBackend = ScriptingBackend.Mono;
        public const Architecture architecture = Architecture.macOS;
        public const Distribution distribution = Distribution.None;
    }
}

