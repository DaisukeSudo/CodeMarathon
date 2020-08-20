% https://atcoder.jp/contests/abs/tasks/abc083_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

number_char(Num, Char) :-
  string_chars(Str, [Char]),
  number_string(Num, Str).

string_numbers(Str, Nums) :-
  string_chars(Str, Chars),
  convlist([Char, Num] >> number_char(Num, Char), Chars, Nums).

digit_sum(Num, Sum) :-
  number_string(Num, Str),
  string_numbers(Str, Nums),
  sum_list(Nums, Sum).

is_match(X, A, B) :-
  digit_sum(X, Y),
  between(A, B, Y).

solve(N, A, B, Ret) :-
  findall(I, between(1, N, I), List1),
  include([X] >> is_match(X, A, B), List1, List2),
  sum_list(List2, Ret).

main :-
  read_number(N),
  read_number(A),
  read_number(B),
  solve(N, A, B, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16066217
