DiffLib2
===

[![build](https://github.com/lassevk/Signals4Net/actions/workflows/build.yml/badge.svg)](https://github.com/lassevk/Signals4Net/actions/workflows/build.yml)
[![codecov](https://codecov.io/github/lassevk/DiffLib2/graph/badge.svg?token=M7F5JUBV7W)](https://codecov.io/github/lassevk/DiffLib2)
[![codeql](https://github.com/lassevk/DiffLib2/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/lassevk/DiffLib2/actions/workflows/github-code-scanning/codeql)

This is intended to be a new major release of [DiffLib](https://github.com/lassevk/DiffLib),
written in .NET 8, C# 12, **hopefully** with major performance improvements, based on new
types, such as `Span<T>` and similar.
 

Status
---

Current status of project is "on hold". `(ReadOnly)Span<T>` is severly limited when dealing with recursive algorithms,
as there is no way to cache intermediate results.

A recursive enumerator can easily keep state like this, but since the entire stack unwinds and all local spans are
discarded, and cannot be temporarily stored between invocations, it seems I need a completely new way to think about
this recursive algorithm for this to work.
