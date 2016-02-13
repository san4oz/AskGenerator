function TeacherRating(option, teacher) {
    var self = this;
    self.id = teacher.id;
    self.name = teacher.name;
    self.image = teacher.image;
    self.value = "";
    self.points = [];
    for (var val in teacher.status) {
        if (teacher.status[val][option]) {
            self.value = teacher.status[val][option];
        }
    }
    self.revoute = self.value ? true : false;
    for (var i = 0; i < 10; i++) {
        self.points[i] = { 'checked': self.value - 1 == i }
    }
    self.enableRevote = function () {
        self.revoute = false;
    };
    self.vote = function (option, $index) {
        self.revoute = true;
        self.value = $index + 1;
    }
}