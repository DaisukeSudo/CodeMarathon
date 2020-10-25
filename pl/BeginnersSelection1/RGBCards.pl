% https://atcoder.jp/contests/abc064/tasks/abc064_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, '\n', ' ', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

result_Y('YES').
result_N('NO').

solve(RGB, Ret) :-
  divmod(RGB, 4, _, R),
  R =:= 0 ->
    result_Y(Ret);
    result_N(Ret).

main :-
  read_number(RGB),
  solve(RGB, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc064/submissions/16797914
