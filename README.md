# RyzenAdjustApi

A C# implementation of the API which allows access to modify SMU (System Management Unit) parameters for Ryzen processor CPUs and APUs.

The original C/C++ implementation can be found as [RyzenAdj](https://github.com/FlyGoat/RyzenAdj).

**NOTE:** This library is intended for 64-bit systems only.

## Installation

The RyzenAdjustApi(.dll) class library built from this solution requires the use of the WinRing0 (Ols - OpenLibSys) class library and drivers.

The following files are present in the solution and copied across to the debug/release folders when building the RyzenAdjustApi solution:

* WinRing0x64.dll
* WinRing0x64.exp
* WinRing0x64.lib
* WinRing0x64.sys


**NOTE:** These files along with the RyzenAdjustApi.dll needs to be present in the same folder in order to use this library.

## Usage

In order to use the library **add a reference** to the RyzenAdjustApi.dll in your project solution.

1. Add a using statement for the RyzenAdjustApi.

```C#
using RyzenAdjustApi
```

2. Create an AdjustApi object to make use of the API.

```C#
AdjustApi api = new AdjustApi()
```

3. Get a ***ryzen_access*** object to use for each call to the API and ensure that you can **use** object by checking for the access.use boolean property.

```C#
ryzen_access access = api.GetRyzenAccess();
if (access.use)
{
    // The access object can be used, perform actions using the API...
} 
else 
{
    // Show error message...
}
```

4. Make calls to change parameters using the api object and passing the **ryzen_access** object and call parameters.

```C#
// E.g. Set the STAPM limit to 25W (25000).
int value = 25000;
bool result = api.SetStapmLimit(access, value);

if (result)
    // Set successfully
else 
    // Error setting parameter.
```

5. When you have finished with the api object, call the **api.Cleanup()** method; there is a finalizer/class deconstructor in the class to automatically cleanup when the garbage collector cleans up.

```C#
api.Cleanup();
```

**NOTE:** All parameters in the API can be found on the [original repository](https://github.com/FlyGoat/RyzenAdj).

## Contributing

Any pull requests or issues relating to the project is welcomed.

## Authors and acknowledgment

* [FlyGoat](https://github.com/FlyGoat/)
