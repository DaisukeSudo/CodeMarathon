% https://atcoder.jp/contests/abc149/tasks/abc149_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

solve(A, B, K, AR, BR) :-
  AR is max(0, A - K),
  M  is max(0, K - A),
  BR is max(0, B - M).

main :-
  read_number(A),
  read_number(B),
  read_number(K),
  solve(A, B, K, AR, BR),
  concat_atom([AR, BR], ' ', Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc149/submissions/29920631
