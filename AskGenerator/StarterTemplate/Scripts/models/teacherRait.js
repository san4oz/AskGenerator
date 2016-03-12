function TeacherRating(option, teacher, $http) {
    var self = this;
    self.id = teacher.id;
    self.name = teacher.name;
    self.image = teacher.image;
    self.value = '';
    self.selected = ''
    self.points = [];
    for (var val in teacher.status) {
        if (teacher.status[val].id == option) {
            self.selected = self.value = teacher.status[val].value;
            break;
        }
    }

    for (var i = 0; i < 10; i++) {
        self.points[i] = { 'checked': self.value - 1 == i }
    }

    var sendVote = function (option, $index, token, success) {
        var data = { id: self.id, questionId: option, answer: $index + 1, '__RequestVerificationToken': token };
        $.post('/home/addAnswer', data, function () {
            GTM.trackVote(self.selected);
            self.selected = self.value = $index + 1;
            if (success)
                success();
        }).error(function (e) {
            if ((e.status == 403 || e.status == 401) && typeof (e.responseText) != 'string')
                location.assign('/login');
            else {
                var response = JSON.parse(e.responseText);
                if (response.url)
                    location.assign(response.url);
                else
                    console.error(e.responseText);
            }
            console.log('Woooops, something going wrong' + JSON.stringify(e));
        });
    }
    self.vote = function (option, $index, token, success) {
        GTM.trackVoteClick(self.selected);
        if (!self.selected) sendVote(option, $index, token, success);
        else if (self.selected !== self.value) {
            if (confirm('Change vote ?')) sendVote(option, $index, token, success);
            else self.mouseOut();
        }

    };

    self.mouseOver = function (index) {
        self.value = index + 1;
    };

    self.mouseOut = function () {
        self.value = self.selected;
    };
}