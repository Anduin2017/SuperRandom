# SuperRandom

[![NuGet version (Newtonsoft.Json)](https://img.shields.io/nuget/v/Anduin.SuperRandom.svg?style=flat-square)](https://www.nuget.org/packages/Anduin.SuperRandom/)

[中文](./README.zh.md)

Non-repeating random numbers.

## How to install

This project is built based on .NET Standard 2.1.

Powershell:

```powershell
PM > Install-Package Anduin.SuperRandom
```

Others:

```bash
$ > dotnet add package Anduin.SuperRandom
```

## Why do I need this

The traditional methods for obtaining `n` non-repeating random numbers are:

* The random number is generated by the linear congruence method, and each random number is generated and compared in the database. If it already exists, the number is discarded.
* Randomly generate a linear sequence, and then shuffle the sequence.

The time complexity of the above traditional method is `O (n ^ 2)`. Especially in the case of larger data, for the first algorithm: after the generation of a large number of numbers already exists, the performance will become slower and slower. For the second method, it will take up very high memory space.

`SuperRandom` is based on the RSA algorithm, whose time complexity is `O (n)` and space complexity is `O (1)`. It is only linearly related to the number of random numbers required, and does not need to be compared with any data set. So the performance is fast. Especially when it is over one million, it is obviously superior to any random number algorithm that depends on storage.

## How to use

```csharp
using Anduin.SuperRandom;

foreach (var rand in new Randomizer().GetRandomNumbers(1000000))
{
    Console.WriteLine("Got random number: " + rand);
}
```

## Demo

For example, call this method to generate 15 non-repeat random numbers less than 15:

```log
Got random number: 9
Got random number: 10
Got random number: 14
Got random number: 4
Got random number: 5
Got random number: 6
Got random number: 0
Got random number: 1
Got random number: 11
Got random number: 12
Got random number: 13
Got random number: 2
Got random number: 3
Got random number: 7
Got random number: 8
```

## Recommended scenarios

* Generate unique invitation codes, and lottery codes
* Generate non-incrementing non-duplicate primary key (for example, generate ID number)
* Other scenes that require non-repeating random numbers

Note: Not recommended for use in lottery tickets, verification codes, etc. The random numbers generated by this algorithm are weak pseudo-random numbers, especially the randomness at the beginning is poor. It is possible to get the whole sequence through brute force calculation.

## What it does

`` `text
N is a positive integer.
Let a positive integer A be distributed in the set [2, (N-1)]
C = (A ^ D) mod N
And meet the following conditions:

D is prime
N is the product of two prime numbers (P, Q)
P! = Q
(D * E) mod ((P-1) * (Q-1)) = 1

∵ C = (A ^ D) mod N
∴ A = (C ^ E) mod N
∴ C and A are one-to-one mapping
∵ A is distributed in the set [2, (N-1)]
∴ A does not repeat, no omission
∴ C does not repeat, no omission
```
