(function(factory){
    if(typeof define==='function'&&define.amd){
        define(['jquery'],factory);
    }else if(typeof module==='object'&&module.exports){
        module.exports=factory(require('jquery'));
    }else{
        factory(window.jQuery);
    }
}(function($){
    $.extend(true,$.summernote.lang,{
        'en-US':{ /* English */
            math:{
                dialogTitle:'Formula muharriri',
                tooltip:'Matematik formula',
                pluginTitle:'Insert math',
                ok:"Qo'yish",
                cancel:'Bekor'
            }
        }
    });
    $.extend($.summernote.options,{
        math:{
            icon:'<b>&sum;</b>'
        }
    });
    $.extend($.summernote.plugins,{
        'math': function (context) {
             var self=this,
            ui=$.summernote.ui;
           $note=context.layoutInfo.note;
           $editor=context.layoutInfo.editor;
           $editable=context.layoutInfo.editable;
           options=context.options;
            lang = options.langInfo;
          
            self.events = {
                'summernote.keyup summernote.mouseup summernote.change summernote.scroll': function () {

                    self.update();

                },
                'summernote.disable summernote.dialog.shown': function () {
                    self.hide();
                }
            };


           



            context.memo('button.math',function(){
                var button=ui.button({
                    contents:options.math.icon,
                    tooltip:lang.math.tooltip,
                    click:function(e){
                        // Cursor position must be saved because is lost when popup is opened.
                        context.invoke('editor.saveRange');
                        context.invoke('math.show');
                    }
                });
                return button.render();
            });

            self.initialize = function () {


                var $container=options.dialogsInBody?$(document.body):$editor;
                var body = '<div class="form-group"><p>Type <a href="https://khan.github.io/KaTeX/function-support.html" target="_blank">LaTeX markup</a> here: </p> <p><input id="note-latex" onkeyup="Prewiew(this);" class="form-control"></p><p>Korinishi: </p><div style="min-height:20px;"><span class="note-math-dialog"></span></div></div>';
                self.$dialog=ui.dialog({
                    title:lang.math.dialogTitle,
                    body:body,
                    footer:'<button class="btn btn-primary note-math-btn">'+lang.math.ok+'</button>'
                }).render().appendTo($container);

                self.$popover = ui.popover({
                    style: 'left:20%; top: 90%; display:block;',
                  className: 'note-math-popover'
                }).render().appendTo(options.container);
                const $content = self.$popover.find('.popover-content,.note-popover-content');

                context.invoke('buttons.build', $content, ['math']);
            };

            self.hasMath = function(node) {
              return node && $(node).hasClass('note-math');
            };

            self.isOnMath = function(range) {
              const ancestor = $.summernote.dom.ancestor(range.sc, self.hasMath);
              return !!ancestor && ancestor === $.summernote.dom.ancestor(range.ec, self.hasMath);
            };

            self.update = function () {
                // Prevent focusing on editable when invoke('code') is executed
                if (!context.invoke('editor.hasFocus')) {
                    self.hide();
                    return;
                }

                const rng = context.invoke('editor.getLastRange');
                if (rng.isCollapsed() && self.isOnMath(rng)) {
                    const node = $.summernote.dom.ancestor(rng.sc, self.hasMath);
                    const latex = $(node).find('.note-latex');

                    if (latex.text().length !== 0) {
                     
                        self.$popover.find('button').html(latex.text());
                        const pos = $.summernote.dom.posFromPlaceholder(node);
                        self.$popover.css({
                            background: '#bde88d',
                            display: 'block',
                            left: '50%',
                            top: '90%'
                        });
                    } else {
                        self.hide();
                    }
                } else {
                    self.hide();
                }
            };

            self.hide = function () {
                self.$popover.hide();
            };

            self.destroy = function () {

                ui.hideDialog(this.$dialog);
              
            

                self.$dialog.remove();
                self.$popover.remove();
            };

            self.bindEnterKey = function($input,$btn){
                $input.on('keypress',function(event){
                    if(event.keyCode===13)$btn.trigger('click');
                });
            };

            self.bindLabels = function(){
                self.$dialog.find('.form-control:first').focus().select();
                self.$dialog.find('label').on('click',function(){
                    $(this).parent().find('.form-control:first').focus();
                });
            };

            self.show = function(){

                var $mathSpan = self.$dialog.find('.note-math-dialog');
                var $latexSpan = self.$dialog.find('#note-latex');


                var $selectedMathNode = self.getSelectedMath();
                
                if ($selectedMathNode === null) {
                    // reset the dialog input and math
                    $mathSpan.empty();
                    $latexSpan.val('');
                   
                }
                else {
                   
                    // edit the selected math node
                    // get the hidden LaTeX markup from the selected math node
                   
                  
                    var hiddenLatex = $selectedMathNode.find('.note-latex').text().replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");;
                    $latexSpan.val(hiddenLatex);
                    katex.render(hiddenLatex, $mathSpan[0]);

                 

                  
                }


                var mathInfo = {}; // not used

                self.showMathDialog(mathInfo).then(function(mathInfo){
                    ui.hideDialog(self.$dialog);
                    var $mathNodeClone = $mathSpan.clone();
                    var $latexNode = $('<span>');
                    $latexNode.addClass('note-latex')
                        .css('display', 'none')
                        .text($latexSpan.val())
                        .appendTo($mathNodeClone);

                    // So we don't pick up the dialog node when selecting math nodes in the editor
                    $mathNodeClone.removeClass('note-math-dialog').addClass('note-math');

                    // We restore cursor position and element is inserted in correct pos.
                    context.invoke('editor.restoreRange');
                    context.invoke('editor.focus');

                    if ($selectedMathNode === null)
                        context.invoke('editor.insertNode',$mathNodeClone[0]);
                    else {// if we are editing an existing mathNode, just replace the contents:
                        if( $.trim($latexNode.html())==='') { // unless there's nothing there, then devare the node
                            $selectedMathNode.remove();
                        }
                        else {
                          
                           
                            $selectedMathNode.html($mathNodeClone.html().replace(/[\r\n]+/gm, ""));
                        }

                    }
                });
            };

            self.showMathDialog = function(editorInfo) {
                return $.Deferred(function (deferred) {
                    var $editBtn = self.$dialog.find('.note-math-btn');
                    ui.onDialogShown(self.$dialog, function () {

                        context.triggerEvent('dialog.shown');
                        $editBtn.click(function (e) {
                            e.preventDefault();
                            deferred.resolve({

                            });
                        });
                        self.bindEnterKey($editBtn);
                        self.bindLabels();
                    });
                    ui.onDialogHidden(self.$dialog, function () {
                        $editBtn.off('click');
                        if (deferred.state() === 'pending') deferred.reject();
                    });
                    ui.showDialog(self.$dialog);
                });
            };

            self.getSelectedMath = function() {
                var selection = window.getSelection();
                if( selection ){
                    // get all math nodes
                    var $selectedMathNode = null;
                    var $mathNodes = $('.note-math');
                    $mathNodes.each(function() {
                        // grab first math node in the selection (including partial).
                        if(selection.containsNode(this, true)) {
                            $selectedMathNode = $(this);
                        }
                    });
                    return $selectedMathNode;
                }

            };


        }
    });
}));
