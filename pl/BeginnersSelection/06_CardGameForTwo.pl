% https://atcoder.jp/contests/abs/tasks/abc088_b

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

draw([], _, 0, 0).
draw([X|Xs], true, A + X, B) :-
  draw(Xs, false, A, B).
draw([X|Xs], false, A, B + X) :-
  draw(Xs, true, A, B).

solve([], 0).
solve(As, Ret) :-
  sort(0, @>=, As, Sorted),
  draw(Sorted, true, A, B),
  Ret is A - B.

main :-
  read_number(N),
  read_numbers(N, As),
  solve(As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16086342
