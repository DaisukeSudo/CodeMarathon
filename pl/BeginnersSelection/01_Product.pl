% https://atcoder.jp/contests/abs/tasks/abc086_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

format_result(0, 'Even').
format_result(1, 'Odd').

solve(A, B, Ret) :-
  A1 is A /\ 1,
  B1 is B /\ 1,
  R1 is A1 /\ B1,
  format_result(R1, Ret).

main :-
  read_number(A),
  read_number(B),
  solve(A, B, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16065955
