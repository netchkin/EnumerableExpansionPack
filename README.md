# Enumerable Expansion Pack

So there were a few operation I was missing from IEnumerable default implementation, so here are they. The goal is to create an efficient implementation that's readable at the same time.

## Diff

Takes two collections and produces a three-way diff. Once called, it goes through all members of the both collections (there's no laziness that's common for IEnumerable operations).

## SplitBy

Splits a collections by a condition into two new collections. Once called, it goes through all the members of the moth collections (there's no laziness that's common for IEnumerable operations).
