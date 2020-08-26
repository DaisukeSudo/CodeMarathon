% https://atcoder.jp/contests/abs/tasks/abc085_c

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).
 
read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).
 
goal(A, N, Y) :-
  between(0, N, A),
  X is (Y - N - 9 * A),
  X mod 4 =:= 0,
  X >= 0,
  N - A - X / 4 >= 0.
 
result_exists(A, N, Y, Ret) :-
  B is (Y - N - 9 * A) / 4,
  C is N - A - B,
  atomics_to_string([A, ' ', B, ' ', C], Ret).
result_not_exists('-1 -1 -1').
 
solve([], 0).
solve(N, Y, Ret) :-
  Y2 is Y / 1000,
  findnsols(1, A, goal(A, N, Y2), L),
  [A] = L ->
    result_exists(A, N, Y2, Ret);
    result_not_exists(Ret).
 
main :-
  read_number(N),
  read_number(Y),
  solve(N, Y, Ret),
  write(Ret).
 
:- main.

% https://atcoder.jp/contests/abs/submissions/16246183
