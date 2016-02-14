function VoteOption(option, teachers, $http) {
    var self = this;
    self.id = option.id;
    self.label = option.label;
    self.teachers = [];
    self.collapsed = true;
    self.voted = 0;
    self.done = false;
    self.collapse = function () {
        self.collapsed = !self.collapsed;
        if (self.collapsed) {
            if (self.voted == self.teachers.length) {
                self.done = true;
            }
        }
    };

    self.vote = function (teacherId, $index) {
        self.teachers[teacherId].vote(self.id, $index);
        self.voted += 1;
    };

    self.enableRevote = function ($index) {
        self.teachers[$index].enableRevote();
        self.voted -= 1;
    };

    var index = 0;
    for (var teacher in teachers) {
        var rating = new TeacherRating(self.id, teachers[teacher], $http);
        rating.index = index++;
        self.voted += rating.value ? 1 : 0;
        self.teachers.push(rating);
    }
    if (self.voted == self.teachers.length) {
        self.done = true;
    }
}