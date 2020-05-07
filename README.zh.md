# SuperRandom

[![NuGet version (Newtonsoft.Json)](https://img.shields.io/nuget/v/Anduin.SuperRandom.svg?style=flat-square)](https://www.nuget.org/packages/Anduin.SuperRandom/)

[中文](./README.zh.md)

不重复随机数。

## 如何安装

基于.NET Standard 2.1构建。

Powershell:

```powershell
PM > Install-Package Anduin.SuperRandom
```

Others:

```bash
$ > dotnet add package Anduin.SuperRandom
```

## 为什么需要这个库

传统获取`n`个不重复的随机数方法有：

* 通过线性同余法生成随机数，并且每生成一个随机数，都在数据库中进行比较，如果已经存在，则抛弃这个数。
* 随机生成线性序列，再对其排序打乱。

上述的传统方法的时间复杂是`O(n^2)`。尤其是较大的数据的情况下，对于第一个算法：生成到后面大量的数已经存在，则性能会越来越慢。对于第二个方法则会占用极高的内存空间。

SuperRandom是基于RSA算法，其时间复杂度是`O(n)`，空间复杂度`O(1)`。仅和需要的随机数数量呈线性关系，不需要和任何数据集合进行比较。因此性能飞快。尤其在百万个规模以上时明显优于任何依赖存储的随机数算法。

## 用法

```csharp
using Anduin.SuperRandom;

foreach (var rand in new Randomizer().GetRandomNumbers(1000000))
{
    Console.WriteLine("Got random number: " + rand);
}
```

## 运行示例

以输入`15`为例，生成15以内的不重复随机数。

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

## 推荐的使用场景

* 生成不重复的邀请码、抽奖代码
* 生成非自增的不重复主键(例如生成身份证号)
* 其它需要不重复的随机数的场景

注意：不建议在彩票、验证码等场景中使用。这个算法生成的随机数是弱伪随机数，尤其开头位置随机性较差。通过暴力计算容易得到随机序列。

## 它的原理是什么

```text
N是正整数。
设正整数 A 分布于集合 [2, (N-1)]
C =（A ^ D) mod N
且满足如下条件：

D是素数
N是两个素数(P,Q)之积
P != Q
(D * E) mod ((P-1) * (Q-1))=1

∵ C=(A ^ D) mod N
∴ A=(C ^ E) mod N
∴ C与A 是一一的映射
∵ A分布于集合[2, (N-1)]
∴ A不重复，无遗漏
∴ C不重复，无遗漏
```
