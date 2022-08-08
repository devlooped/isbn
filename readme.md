![Icon](https://raw.githubusercontent.com/devlooped/isbn/main/assets/img/icon-32.png) ISBN parsing library
============

An ISBN parsing library

[![Version](https://img.shields.io/nuget/vpre/ISBN.svg?color=royalblue)](https://www.nuget.org/packages/ISBN)
[![Downloads](https://img.shields.io/nuget/dt/ISBN.svg?color=green)](https://www.nuget.org/packages/ISBN)
[![License](https://img.shields.io/github/license/devlooped/isbn.svg?color=blue)](https://github.com/devlooped/isbn/blob/main/license.txt)

# Usage

```csharp
if (ISBN.TryParse("9780753557525", out var isbn))
    // Renders: Publisher: 7535, Article: 5752 (Group 0, English language)
    Console.WriteLine($"Publisher: {isbn.Publisher}, Article: {isbn.Article} (Group {isbn.Group}, {isbn.GroupName})");
```

## Attribution

The implementation is mostly a port from https://github.com/inventaire/isbn3, and consumes the 
group information published in that repository.

<!-- include docs/footer.md -->