# MathToWords

This is a simple program that takes in an expression in algebraic (infix) notation and outputs it in a well-defined, English word format.

My word notation is essentially Polish notation/prefix notation in words. It will never be ambiguous, but it isn't all that easy for humans to keep track of. This program and format is mostly intended as a little toy project, but the format could theoretically be used for a more natural, but defined, way to speak mathematical expressions.

## Explanation

My word notation works much like prefix notation, so the operator is "placed" before its operand(s). For example, addition ("+") is written this way: "the sum of {a} and {b}". Prefix notation does get a bit hard to work with when you have a complicated expression, but the important thing to remember is that each operator has a fixed number of operands. So addition, for example, will always have two operands and thus will always be in the format described above. See the table below for the list of currently supported operations, and see below that for examples of the format in action.

| Operator | Text Format |
| --- | --- |
| a + b | the sum of a and b |
| a - b | the difference of a and b |
| a * b | the product of a and b |
| a / b | the quotient of a and b |
| a ^ b | the result of a to the power of b |
| a % b | the result of a modulus b |

## Examples

3+2 becomes:

    the sum of three plus two

27 * 5 - 5 becomes:

    the difference of the product of twenty seven and five and five

(24 * (9 + (6 * 76))) / 2 becomes:

    the quotient of the product of twenty four and the sum of nine and the product of seventy six and two