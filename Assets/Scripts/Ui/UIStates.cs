using System;

[Flags]
public enum UIStates
{
    NONE = 0,
    INVENTORY = 1 << 0,
    TOOL      = 1 << 1,
    CONTAINER = 1 << 2
}