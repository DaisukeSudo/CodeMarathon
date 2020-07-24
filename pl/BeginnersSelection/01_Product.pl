% https://atcoder.jp/contests/abs/tasks/abc086_a

read_string(String) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, String).

read_number(Number) :-
  read_string(String),
  number_string(Number, String).

format_result(0, 'Even').
format_result(1, 'Odd').

main :-
  read_number(A),
  read_number(B),
  A1 is A /\ 1,
  B1 is B /\ 1,
  R1 is A1 /\ B1,
  format_result(R1, R2),
  write(R2).

:- main.

% https://atcoder.jp/contests/abs/submissions/15389747
