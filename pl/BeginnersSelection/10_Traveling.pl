% https://atcoder.jp/contests/abs/tasks/arc089_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers_3(0, []).
read_numbers_3(N, [(T, X, Y)|Xs]) :-
  read_number(T),
  read_number(X),
  read_number(Y),
  N1 is N - 1,
  read_numbers_3(N1, Xs).

move(_, _, _, []).
move(T1, X1, Y1, [(T2, X2, Y2)|Ps]) :-
  (T2 /\ 1) =:= (X2 + Y2) /\ 1,
  (T2 - T1) >= (abs(X2 - X1) + abs(Y2 - Y1)),
  move(T2, X2, Y2, Ps).

result_Y('Yes').
result_N('No').

solve(Ps, Ret) :-
  move(0, 0, 0, Ps) ->
    result_Y(Ret);
    result_N(Ret).

main :-
  read_number(N),
  read_numbers_3(N, Ps),
  solve(Ps, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16265583
