# omf_csharp

An incubator repository for the C# implementation of the
[Open Mining Format (*.omf)](https://github.com/GMSGDataExchange/omf/),
a new standard for mining data backed by the
[Global Mining Standards & Guidelines Group](http://www.globalminingstandards.org/).

<aside class="warning">
**Pre-Release Notice**

This library is compatible with a beta version (v0.9.1) Open Mining
Format and the associated Python API. The storage format and
libraries might be changed in backward-incompatible ways and are not
subject to any SLA or deprecation policy.
</aside>

## Why

Support of multiple languages and frameworks should facilitate early adoption
of Open Mining Format. This library exists for rapid development of a usable
.NET interface written in C#.

## Goals

* Read *.omf files into a class structure that directly parallels the Python API
* Possibly write these files as well
* Leverage type-checking built in to C#, deserialization from Newton, etc.
* Rapidly develop a working solution

## Not Goals

* This does not attempt to be as fully-featured as the Python API library

## Scope

This repository is only intended for initial development of the C# library.
The initial development should result in a working interface developers may
use to begin including Open Mining Format support in their .NET applications.

After reaching this initial goal, effort will shift towards building a
templating language that can auto-generate and auto-update the C#
library from the Python library, eliminating the need for separate
development of the two libraries.
