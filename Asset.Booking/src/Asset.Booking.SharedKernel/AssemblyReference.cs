﻿namespace Asset.Booking.SharedKernel;
using System.Reflection;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
