#MathToWords

This is a simple program that takes in an expression in algebraic (infix) notation and outputs it in words.

My word notation is essentially Polish notation/prefix notation in words. It will never be ambiguous, but it isn't all that easy for humans to keep track of. This program and format is mostly intended as a little toy project, but the format could theoretically be used for a more natural, but defined, way to speak mathematical expressions.

##Explanation

| Operator | Text Format |
| --- | --- |
| a + b | the sum of a and b |

##Examples

3+2 becomes:

    the sum of three plus two

27 * 5 - 5 becomes:

    the difference of the product of twenty seven and five and five

(24 * (9 + (6 * 76))) / 2 becomes:

    the quotient of the product of twenty four and the sum of nine and the product of seventy six and two