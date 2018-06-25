$('.index .carOne img').playKeyframe({
    name: 'opacity-car',
    duration: '0.5s',
    delay: '0.5s',
    iterationCount: 1,
    direction: 'normal',
    fillMode: 'forwards',
    complete: function(){

        $('.index .carLeft img').playKeyframe({
            name: 'left-car',
            duration: '0.5s',
            delay: '0s',
            iterationCount: 1,
            direction: 'normal',
            fillMode: 'forwards'
        });
        $('.index .carRight img').playKeyframe({
            name: 'right-car',
            duration: '0.5s',
            delay: '0s',
            iterationCount: 1,
            direction: 'normal',
            fillMode: 'forwards',
            complete:function(){
                $('.carOneDescription').fadeIn().addClass('selected');
                $('.carLeftDescription').fadeIn().addClass('selected');
                $('.carRightDescription').fadeIn().addClass('selected');
                setInterval(function(){$('span.centerspan').fadeIn(600)}, 1500);
                setInterval(function(){$('span.rightspan').fadeIn(600)}, 1500);
                setInterval(function(){$('span.leftspan').fadeIn(600)}, 1500);
            }
        });

    }
});
$.keyframe.define({
    name: 'opacity-car',
    from: { 'opacity': 0},
    to  : { 'opacity': 1}
});
$.keyframe.define({
    name: 'right-car',
    from: { 'margin-left': '-890px'},
    to  : { 'margin-left': '0'}
});
$.keyframe.define({
    name: 'left-car',
    from: { 'margin-right': '-890px'},
    to  : { 'margin-right': '0'}
});


$('button').hover(function(){
    $(this).addClass('selected');
});