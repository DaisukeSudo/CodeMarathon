% https://atcoder.jp/contests/abc088/tasks/abc088_c

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers(0, []).
read_numbers(N, [X|Xs]) :-
  read_number(X),
  N1 is N - 1,
  read_numbers(N1, Xs).

read_numbers2(0, _, []).
read_numbers2(H, W, [Xs|Xss]) :-
  read_numbers(W, Xs),
  H1 is H - 1,
  read_numbers2(H1, W, Xss).

satisfy([[C11, C12, C13], [C21, C22, C23], [C31, C32, C33]]) :-
  between(0, 100, A1),
  between(0, 100, A2),
  between(0, 100, A3),
  B1 is C11 - A1,
  B2 is C22 - A2,
  B3 is C33 - A3,
  C12 =:= A1 + B2,
  C13 =:= A1 + B3,
  C21 =:= A2 + B1,
  C23 =:= A2 + B3,
  C31 =:= A3 + B1,
  C32 =:= A3 + B2.

result_y('Yes').
result_n('No').

solve(Cs, Ret) :-
  satisfy(Cs) ->
    result_y(Ret);
    result_n(Ret).

main :-
  read_numbers2(3, 3, Cs),
  solve(Cs, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc088/submissions/17333521
