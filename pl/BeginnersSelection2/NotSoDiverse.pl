% https://atcoder.jp/contests/abc081/tasks/arc086_a

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

count_in_list(N, [], C, C).
count_in_list(N, [X|Xs], A, C) :-
  (N =:= X -> A1 is A + 1; A1 is A),
  count_in_list(N, Xs, A1, C).
count_in_list(N, List, C) :-
  count_in_list(N, List, 0, C).

count_by_group([], _, Cs, Cs).
count_by_group([X|Xs], List, As, Cs) :-
  count_in_list(X, List, A),
  count_by_group(Xs, List, [A|As], Cs).
count_by_group(Xs, List, [A|As], Cs).
count_by_group(Distincted,List, C) :-
  count_by_group(Distincted, List, [], C).

take_number(0, _, 0) :- !.
take_number(_, [], 0) :- !.
take_number(N, [X|Xs], C) :-
  N1 is N - 1,
  take_number(N1, Xs, C1),
  C is C1 + X.

solve(N, K, As, Ret) :-
  sort(0, @>, As, Distincted),
  length(Distincted, DistinctedSize),
  ChangeCount is max(0, DistinctedSize - K),
  count_by_group(Distincted, As, GroupCounts),
  sort(0, @=<, GroupCounts, Sorted),
  take_number(ChangeCount, Sorted, Ret).

main :-
  read_number(N),
  read_number(K),
  read_numbers(N, As),
  solve(N, K, As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc081/submissions/17326076 (TLE)
