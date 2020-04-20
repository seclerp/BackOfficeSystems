<template>
    <div class="page-container">
        <md-app md-mode="fixed">
            <md-app-toolbar class="md-primary">
                <md-button class="md-icon-button" @click="menuVisible = true">
                    <md-icon>menu</md-icon>
                </md-button>
                <span class="md-title">Brands</span>
            </md-app-toolbar>

            <md-app-drawer :md-active.sync="menuVisible">
                <md-toolbar class="md-transparent" md-elevation="0">Navigation</md-toolbar>

                <md-list>
                    <md-list-item to="/" @click="menuVisible = false">
                        <md-icon>move_to_inbox</md-icon>
                        <span class="md-list-item-text">Sum of inventory</span>
                    </md-list-item>

                    <md-list-item to="/brand/new" @click="menuVisible = false">
                        <md-icon>insert_drive_file</md-icon>
                        <span class="md-list-item-text">Create brand</span>
                    </md-list-item>
                </md-list>
            </md-app-drawer>

            <md-app-content>
                <router-view/>
            </md-app-content>
        </md-app>
    </div>
</template>

<style lang="scss" scoped>
    .md-app {
        max-height: 100vh;
        min-height: 100vh;
    }
</style>

<script>
    export default {
        name: 'App',
        data: () => ({
            menuVisible: false
        })
    }
</script>