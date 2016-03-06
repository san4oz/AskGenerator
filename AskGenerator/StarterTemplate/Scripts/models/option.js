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

    var token = $('input[name="__RequestVerificationToken"]').val();

    self.vote = function (teacherIndex, $index) {
        for (var i in self.teachers) {
            if (self.teachers[i].id == teacherIndex) {
                self.teachers[i].vote(self.id, $index, token);
                self.voted += 1;
                break;
            }
        }
    };

    var index = 0;
    for (var teacher in teachers) {
        var rating = new TeacherRating(self.id, teachers[teacher], $http);
        rating.index = index++;
        self.voted += rating.value > 0 ? 1 : 0;
        self.teachers.push(rating);
    }
    if (self.voted == self.teachers.length) {
        self.done = true;
    }
}