import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ArrayService {

  /**
   * Checks if two number arrays are equal.
   * Two arrays are considered equal if they have the same length
   * and contain the same elements in the same order.
   * @param arr1 First array of numbers
   * @param arr2 Second array of numbers
   * @returns Boolean indicating if arrays are equal
   */
  areNumberArraysEqual(arr1: number[], arr2: number[]): boolean {
    if (arr1.length !== arr2.length) return false;

    const sortedArr1 = [...arr1].sort((a, b) => a - b);
    const sortedArr2 = [...arr2].sort((a, b) => a - b);

    for (let i = 0; i < sortedArr1.length; i++) {
        if (sortedArr1[i] !== sortedArr2[i]) {
            return false;
        }
    }

    return true;
  }

  /**
   * Returns all elements in the target array that are not present in the source array.
   * @param source Source array of numbers
   * @param target Target array of numbers
   * @returns Array of numbers missing in the source array
   */
  getMissingInSource(source: number[], target: number[]): number[] {
    const sourceSet = new Set(source);
    return target.filter(item => !sourceSet.has(item));
  }
}
