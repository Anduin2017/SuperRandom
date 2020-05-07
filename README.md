# SuperRandom

不重复随机数。

## 用法

```csharp
foreach (var rand in GetRandomNumbers(1000000).Take(100))
{
  Console.WriteLine("Got random number: " + rand);
}
```

## 为什么需要这个库？

传统随机数方法无法保证得到的数不重复。因此，大部分情况下的解决方案，都是每生成一个随机数，都在数据库中进行比较，如果已经存在，则抛弃这个数。

但是上述的传统方法的时间复杂是`O(n^2)`。尤其是较大的数据的情况下，生成到后面会越来越慢。

SuperRandom的时间复杂度是`O(n)`。仅和需要的随机数数量呈线性关系，不需要和任何数据集合进行比较。因此性能飞快。

## 它的原理是什么？

```
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

