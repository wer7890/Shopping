Vue.prototype.$bus = new Vue();

var vm = new Vue({
    el: '#app',
    template: `
        <div class="container mt-5">
            <div class="row mt-2">
                <menu-component></menu-component>
                <frame-component></frame-component>
            </div>
        </div>
    `,
    components: {
        'menu-component': MenuComponent,
        'frame-component': FrameComponent,
    }
});