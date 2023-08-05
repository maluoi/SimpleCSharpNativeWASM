using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;

// Prevent warnings by throwing an exception when not running on a browser
if (!OperatingSystem.IsBrowser())
	throw new PlatformNotSupportedException("This code is expected to run on browsers!");

// This is needed before we can import or run functions from JSApi.js
await JSHost.ImportAsync("JSApi.js", "./JSApi.js");

Console.WriteLine("Hello, console!");
JSApi.DocOutput("Hello document output!");
JSApi.DocOutput("Here's a number from C: " + CApi.get_number());
JSApi.DocOutput("Here's a number from C, using C# callbacks: " + CApi.modify_number(x=>x*2));


// Bind functions from JSApi.js
public static partial class JSApi
{
	[JSImport("docOutput", "JSApi.js")]
	public static partial void DocOutput(string message);
}

// Bind functions from capi.c
public static class CApi
{
	[DllImport("capi")]
	public static extern int get_number();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int CallbackDelegate(int number);

	[DllImport("capi")]
	public static extern int modify_number([MarshalAs(UnmanagedType.FunctionPtr)] CallbackDelegate modify);
}