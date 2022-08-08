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
# Sponsors 

<!-- sponsors.md -->
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![C. Augusto Proiete](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/augustoproiete.png "C. Augusto Proiete")](https://github.com/augustoproiete)
[![SandRock](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sandrock.png "SandRock")](https://github.com/sandrock)
[![Amazon Web Services](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/aws.png "Amazon Web Services")](https://github.com/aws)
[![Christian Findlay](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MelbourneDeveloper.png "Christian Findlay")](https://github.com/MelbourneDeveloper)
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- docs/footer.md -->
